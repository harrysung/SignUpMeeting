var model = {
    view: ko.observable("welcome"),
    attendee: {
        name: ko.observable(""),
        email: "",
        phone: "",
        isSpeaker: ko.observable("true")
    },
    attendees: ko.observableArray([])
}
var showForm = function () {
    model.view("form");
}
var sendRsvp = function () {
    $.ajax("/api/Attendees", {
        type: "POST",
        data: {
            name: model.attendee.name(),
            email: model.attendee.email,
            phone: model.attendee.phone,
            isSpeaker: model.attendee.isSpeaker()
        },
        success: function () {
            getAttendees();
        }
    });
}
var getAttendees = function () {
    $.ajax("/api/Attendees", {
        type: "GET",
        success: function (data) {
            model.attendees.removeAll();
            model.attendees.push.apply(model.attendees, data.map(function (attendee) {
                return attendee.Name;
            }));
            model.view("thanks");
        }
    });
}
$(document).ready(function () {
    ko.applyBindings();
})