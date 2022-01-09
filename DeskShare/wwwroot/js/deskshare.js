


function addToggleEvents(){

var show = function (elem) {

    if (elem.parentElement != null) {
        show(elem.parentElement);
    }


    // Get the natural height of the element
    var getHeight = function () {
        elem.style.display = 'block'; // Make it visible
        var height = elem.scrollHeight + 'px'; // Get it's height
        elem.style.display = ''; //  Hide it again
        return height;
    };

    var height = getHeight(); // Get the natural height
    elem.classList.add('is-visible'); // Make the element visible
    elem.style.height = height; // Update the max-height

    elem.style.height = '';
    // Once the transition is complete, remove the inline max-height so the content can scale responsively
    //window.setTimeout(function () {
        
    //}, 0);

    
   
};

// Hide an element
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
var toggle = function (elem, timing) {

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
    console.log(event);
    // Make sure clicked element is our toggle
    if (!event.target.classList.contains('toggle')) return;

    console.log("contains toggle");
    // Prevent default link behavior
    event.preventDefault();

    // Get the content
    var content = document.querySelector(event.target.hash);
    if (!content) return;

    // Toggle the content
    toggle(content);
}, false);
}




function createBooking(id) {

    var bookingModel = {
        _Start: $("#in_dateTimePicker_" + id).val(),
        _End: $("#out_dateTimePicker_" + id).val(),
        _Desk: id
    }

    window.$.ajax({
        url: window.Urls.createbookings,
        data: {
            bookingModel: bookingModel
        },
        type: "POST",
        success: function () {
            getAllBookings();
            getMyBookings();
        }, error: {

        }
    });
}
function deleteBooking(id) {

    window.$.ajax({
        url: window.Urls.deletebookings,
        data: {
            id: id
        },
        type: "DELETE",
        success: function () {
            getAllBookings();
            getMyBookings();
        }, error: {

        }
    });
}




function getAllBookings() {

    console.log("getAllBookings");

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

    console.log(filterModel);

    $("#loaderMain").css("display", "block");
    $("#main").css("display", "none");
    

    window.$.ajax({
        url: window.Urls.getAllbookings,
        type: "GET",
        data: { filterModelString: JSON.stringify(filterModel)},
        success: function (data) {
            $("#allbookings").html(data);

            $("#loaderMain").css("display", "none");
            $("#main").css("display", "block");

            console.log("getAllBookings sucess");
        }, error: function () {
            console.log("getMyBookings failed");
        }
    });
}
function getMyBookings() {
    console.log("getMyBookings");

    $("#loader").css("display", "block");
    
    window.$.ajax({
        url: window.Urls.getMybookings,
        type: "GET",
        success: function (data) {

            $("#lastbookings").html(data);
            $("#loader").css("display", "none");
                   
            console.log("getMyBookings success");
            console.log("data: " + data);
            console.log("html: " + $("#lastbookings").html)
        }, error: function() {
            console.log("getMyBookings failed");
        }
    });
}
