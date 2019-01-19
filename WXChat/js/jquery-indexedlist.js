/**
 * Created by Liujx on 2017-07-18 15:19:43
 */
;
(function(global, $, doc) {
    'use strict';

    var IndexedList = function(options) {
        this.options = options || {};
        this.pointElement = null;
        this.defaults = {
            $bar: $('.mui-indexed-list-bar'), // 存储索引的盒子
            $inner: $('.mui-indexed-list-inner'), // 存储列表盒子
            $alert: $('.mui-indexed-list-alert') // 提示索引的盒子
        }
        this.opts = $.extend({}, this.defaults, options);
        this.bindEvent();
    }

    IndexedList.prototype = {
        constructor: IndexedList,
        scrollTo: function(group) {
            var groupElement = $('[data-group="' + group + '"]')[0];
            if (!groupElement) return false;
            this.opts.$inner.scrollTop(groupElement.offsetTop);
        },
        _findStart: function(event) {
            var self = this;
            if (self.pointElement) {
                $(self.pointElement).removeClass('active');
                self.pointElement = null;
            }
            self.opts.$bar.addClass('active');
            var point = event.changedTouches ? event.changedTouches[0] : event;
            self.pointElement = doc.elementFromPoint(point.pageX, point.pageY);
            if (self.pointElement) {
                var group = self.pointElement.innerText;
                if (group && group.length == 1) {
                    $(self.pointElement).addClass('active');
                    self.opts.$alert.html(group);
                    self.opts.$alert.addClass('active');
                    self.scrollTo(group);
                }
            }
            event.preventDefault();
        },
        _findEnd: function(event) {
            var self = this;
            self.opts.$alert.removeClass('active');
            self.opts.$bar.removeClass('active');
            if (self.pointElement) {
                $(self.pointElement).removeClass('active');
                self.pointElement = null;
            }
        },
        bindBarEvent: function() {
            var self = this;
            self.opts.$bar[0].addEventListener('touchmove', function(event) {
                self._findStart(event);
            }, false);
            self.opts.$bar[0].addEventListener('touchstart', function(event) {
                self._findStart(event);
            }, false);
            doc.addEventListener('touchend', function(event) {
                self._findEnd(event);
            }, false);
            doc.addEventListener('touchcancel', function(event) {
                self._findEnd(event);
            }, false);
        },
        bindEvent: function() {
            this.bindBarEvent();
        }
    }

    global.IndexedList = IndexedList;

    $.fn.IndexedList = function(options) {
        new IndexedList(options);
        return this;
    }

})(window, jQuery, document);