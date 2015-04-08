$(function () {
    var AppView = Backbone.View.extend({
        
        el: $("#app"),
        
        initialize: function () {
            this.listenTo(Widgets, 'add', this.addOne);
            this.listenTo(Widgets, 'reset', this.addAll);
            this.listenTo(Widgets, 'all', this.render);

            //test
            Widgets.create({ title: "Empty Widget 1" });
            Widgets.create({ title: "Empty Widget 2" });
            Widgets.create({ title: "Empty Widget 3" });
        },

        render: function () {
            
        },
        
        addOne: function (todo) {
            var view = new WidgetView({ model: todo });
            this.$("#widgets").append(view.render().el);
        },

        // Add all items in the **Todos** collection at once.
        addAll: function () {
            Todos.each(this.addOne, this);
        },
    });
    var App = new AppView();
});