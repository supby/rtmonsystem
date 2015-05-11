$(function () {
    var AppModel = Backbone.Model.extend({
        defaults: function () {
            return { };
        },

        connect: function () {
            var widgetHub = $.connection.widgetHub;
            widgetHub.client.updateWidgetsData = $.proxy(this.updateWidgetsData, this);
            var _this = this;
            $.connection.hub.start().done(function () {
                widgetHub.server.connectRange(Widgets.toJSON());
            });
        },

        updateWidgetsData: function (widgetId, msg) {
            Widgets.detect(function (w) {
                return w.get('Id') == widgetId;
            }).updateWidgetData(msg);
        },
    });

    var AppView = Backbone.View.extend({
        
        el: $("#app"),
        
        initialize: function () {
            this.listenTo(Widgets, 'add', this.addOne);
            this.listenTo(Widgets, 'reset', this.addAll);
            this.listenTo(Widgets, 'all', this.render);

            Widgets.fetch();
            this.model.connect();
        },

        render: function () {
            //this.addAll();
            //this.$el.html(this.template(this.model.attributes));
            //return this;
        },
        
        addOne: function (widget) {
            var view = new WidgetView({ model: widget });
            this.$("#widgets").append(view.render().el);
        },
        
        addAll: function () {
            Widgets.each(this.addOne, this);
        },
    });
    var App = new AppView({ model: new AppModel() });
});