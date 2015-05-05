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

    connect: function() {
        var widgetHub = $.connection.widgetHub;
        widgetHub.client.updateWidgetsData = $.proxy(this.updateWidgetData, this);
        var _this = this;
        $.connection.hub.start().done(function () {
            widgetHub.server.connect(_this.get('Type'));
        });
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
        this.model.connect();
    },

    updateWidgetData: function(msg){
        var modelType = this.model.get("Type");
        if (modelType == 'YahooFinDataSource') {
            this.$el.find('.rtm-widget-data').text(msg);
        }
        if (modelType == 'RandomNumberDataSource') {
            this.$el.find('.rtm-widget-data').text(msg);
        }
    },

    render: function () {
        this.$el.html(this.template(this.model.toJSON()));
        return this;
    }
});