var Widget = Backbone.Model.extend({
    defaults: function () {
        return {
            Title: "",
            Id: "",
            Name: "",
            Description: "",
            Type: ""
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
        if (this.model.get('Type') == 'YahooFinDataSource') {
            $('.rtm-widget-data', this.$el).text(msg[0].query.results.quote.Ask);
        }
        if (this.model.get('Type') == 'RandomNumberDataSource') {
            $('.rtm-widget-data', this.$el).text(msg);
        }
    },

    render: function () {
        this.$el.html(this.template(this.model.toJSON()));
        return this;
    }
});