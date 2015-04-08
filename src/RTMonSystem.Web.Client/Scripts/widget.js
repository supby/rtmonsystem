var Widget = Backbone.Model.extend({
    defaults: function () {
        return {
            title: "Empty Widget"
        };
    }
});

var WidgetList = Backbone.Collection.extend({    
    model: Widget,
    localStorage: new Backbone.LocalStorage("todos-backbone")
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