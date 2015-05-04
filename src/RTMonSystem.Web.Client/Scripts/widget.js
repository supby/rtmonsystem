var Widget = Backbone.Model.extend({
    defaults: function () {
        return {
            Title: "",
            Id: "",
            Name: "",
            Description: "",
            Type: ""
        };
    }
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
        
    },
    render: function () {
        this.$el.html(this.template(this.model.toJSON()));
        return this;
    }
});