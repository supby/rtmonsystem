$(function () {
    var AppView = Backbone.View.extend({
        
        el: $("#app"),
        
        initialize: function () {
            this.listenTo(Widgets, 'add', this.addOne);
            this.listenTo(Widgets, 'reset', this.addAll);
            this.listenTo(Widgets, 'all', this.render);

            Widgets.fetch();
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
    var App = new AppView();
});