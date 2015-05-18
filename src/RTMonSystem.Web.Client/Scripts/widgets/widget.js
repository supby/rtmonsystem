var Widget = Backbone.Model.extend({
    defaults: function () {
        return {
            Id: '',
            Name: '',
            Title: '',
            Description: '',
            SourceType: '',
            ViewType: 0, // 0 - None, 1 - JSONPath
            RefreshRate: 1000
        };
    },

    updateWidgetData: function (msg) {
        this.trigger('updateWidgetData', [msg]);
    },
});

var WidgetList = Backbone.Collection.extend({    
    model: Widget,
    url: '/Home/Widgets',
    //localStorage: new Backbone.LocalStorage("rtmonsys-backbone"),

    parse: function (response) {
        return response.WidgetList;
    }
});

var Widgets = new WidgetList;

var WidgetView = Backbone.View.extend({
    tagName: "div",
    template: _.template($('#widget-template').html()),

    initialize: function () {
        this.listenTo(this.model, 'updateWidgetData', this.updateWidgetData);
    },

    updateWidgetData: function (msg) {
            $('.rtm-widget-data', this.$el).text(msg);
    },

    render: function () {
        this.$el.html(this.template(this.model.toJSON()));
        return this;
    }
});