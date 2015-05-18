var JSONWidget = Widget.extend({
    defaults: function () {
        return {
            Path: ''
        };
    },
});

var JSONWidgetView = WidgetView.extend({
    //template: _.template($('#json-widget-template').html()),

    updateWidgetData: function (msg) {
        $('.rtm-widget-data', this.$el).text(Object.byString(msg, this.model.get('Path')));
    },

    render: function () {
        this.$el.html(this.template(this.model.toJSON()));
        return this;
    }
});