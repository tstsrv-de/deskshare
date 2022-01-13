//create event for toggle location and booking divs
function addToggleEvents() {
    //make element visible
    var show = function (elem) {

        //recursiv show call to show all invisible parent elements
        if (elem.parentElement != null) {
            show(elem.parentElement);
        }

        // Get the natural height of the element by show, get height and hide
        var getHeight = function () {
            elem.style.display = 'block';
            var height = elem.scrollHeight + 'px';
            elem.style.display = '';
            return height;
        };

        //get and update height
        var height = getHeight();
        elem.classList.add('is-visible');
        elem.style.height = height;
        elem.style.height = '';
    };
    // make element invisible
    var hide = function (elem) {

        // Give the element a height to change from
        elem.style.height = elem.scrollHeight + 'px';

        // Set the height back to 0
        window.setTimeout(function () {
            elem.style.height = '0';
        }, 1);

        // When the transition is complete, hide it
        window.setTimeout(function () {
            elem.classList.remove('is-visible');
        }, 0);

    };
    // Toggle element visibility
    var toggle = function (elem) {

        // If the element is visible, hide it
        if (elem.classList.contains('is-visible')) {
            hide(elem);
            return;
        }

        // Otherwise, show it
        show(elem);

    };
    // Listen for click events
    document.addEventListener('click', function (event) {

        // Make sure clicked element have toggle class
        if (!event.target.classList.contains('toggle')) return;

        // Prevent default link behavior
        event.preventDefault();

        // Get content, return if null
        var content = document.querySelector(event.target.hash);
        if (!content) return;

        // Toggle the content
        toggle(content);
    }, false);
}

//Ajax call to add booking
function createBooking(id) {

    //create booking model
    var bookingModel = {
        _Start: $("#in_dateTimePicker_" + id).val(),
        _End: $("#out_dateTimePicker_" + id).val(),
        _Desk: id
    }

    //pass booking model
    window.$.ajax({
        url: window.Urls.createbookingsUrl,
        data: {
            bookingModel: bookingModel
        },
        type: "POST",
        success: function () {
            GetLocationsAndBookings();
            getMyBookings();
        }, error: function (xhr, status, error) {
            alert("Es ist ein Fehler aufgetreten: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
        }
    });
}

//Ajax call to delete booking
function deleteBooking(id) {

    window.$.ajax({
        url: window.Urls.deletebookingsUrl,
        data: {
            id: id
        },
        type: "DELETE",
        success: function () {
            GetLocationsAndBookings();
            getMyBookings();
        }, error: function (xhr, status, error) {
            alert("Es ist ein Fehler aufgetreten: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
        }
    });
}

//Ajax call to get all locations and bookings
function GetLocationsAndBookings() {

    //create filtermodel
    var filterModel = {
        _FreeToday: $("#cbTodayFree").prop('checked'),
        _FreeTomorrow: $("#cbTomorrowFree").prop('checked'),
        _FreeWeek: $("#cbWeekFree").prop('checked'),
        _Mouse: $("#cbMouse").prop('checked'),
        _Computer: $("#cbComputer").prop('checked'),
        _Docking: $("#cbDocking").prop('checked'),
        _Keyboard: $("#cbKeyboard").prop('checked'),
        _NoScreen: $("#cbNoScreens").prop('checked'),
        _OneScreen: $("#cbOneScreens").prop('checked'),
        _TwoScreens: $("#cbTwoScreens").prop('checked'),
        _ThreeScreens: $("#cbThreeScreens").prop('checked')
    }

    //show loading spinner
    $("#loaderMain").css("display", "block");
    $("#main").css("display", "none");

    //ajax call
    window.$.ajax({
        url: window.Urls.GetLocationsAndBookingsUrl,
        type: "GET",
        data: { filterModelString: JSON.stringify(filterModel) },
        success: function (data) {
            //pass recieved view to container
            $("#allbookings").html(data);

            //hide loading spinner
            $("#loaderMain").css("display", "none");
            $("#main").css("display", "block");
        }, error: function (xhr, status, error) {
            alert("Es ist ein Fehler aufgetreten: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
        }
    });
}

//Ajax call to get user bookings
function getMyBookings() {

    //show loading spinner
    $("#loader").css("display", "block");

    window.$.ajax({
        url: window.Urls.getMybookingsUrl,
        type: "GET",
        success: function (data) {

            //pass recieved view to container
            $("#lastbookings").html(data);

            //hide loading spinner
            $("#loader").css("display", "none");
        }, error: function (xhr, status, error) {
            alert("Es ist ein Fehler aufgetreten: " + status + " " + error + " " + xhr.status + " " + xhr.statusText);
        }
    });
}
