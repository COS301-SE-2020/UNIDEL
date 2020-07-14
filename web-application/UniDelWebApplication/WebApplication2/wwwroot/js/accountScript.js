var document;
var userType = "CallCentre"; // CallCentre, FleetManager, Driver

function changeForm()
{
    if (userType == "CallCentre")
    {
        // Nothing to destroy.

    }
    else if (userType == "FleetManager")
    {
        // Destroy Fleet Manager Name
        var Mainparent = document.getElementById("firstN");
        var divparent = document.getElementById("surnDiv");
        var label = document.getElementById("surnlbl");
        var input = document.getElementById("surname");
        var small = document.getElementById("surnameErrorMsg");

        divparent.removeChild(label);
        divparent.removeChild(input);
        divparent.removeChild(small);

        Mainparent.removeChild(divparent);
    }
    else if (userType == "Driver")
    {
        //alert("Destroying Driver");
        // Destroy Surname
        var Mainparent = document.getElementById("firstN");
        var divparent = document.getElementById("surnDiv");
            var label = document.getElementById("surnlbl");
            var input = document.getElementById("surname");
            var small = document.getElementById("surnameErrorMsg");

            divparent.removeChild(label);
            divparent.removeChild(input);
            divparent.removeChild(small);

        Mainparent.removeChild(divparent);
    }

    userType = $("#userType").children("option:selected").val();
    if (userType == "CallCentre")
    {
        CallCentre();
    }
    else if (userType == "FleetManager")
    {
        FleetManager();
    }
    else if (userType == "Driver")
    {
        Driver();
    }
}

function CallCentre()
{
    // nothing special ¯\_(ツ)_/¯
}

function Driver()
{
    // Requires Surname
        //<div class="form-group">
        //    <label for="surname" id="surnlbl">Surname</label>
        //    <input type="text" class="form-control" id="surname" placeholder="Enter surname" required>
        //    <small id="surnameErrorMsg" class="error-msg text-danger" hidden>Invalid surname, please enter only alphabetical characters</small>
        //</div>

    // div
    var div = document.createElement("div");
    div.setAttribute("class", "form-group");
    div.setAttribute("id", "surnDiv");

    var element = document.getElementById("firstN");
    element.appendChild(div);

        // label
        var label = document.createElement("LABEL");
        label.setAttribute("for", "surname");
        label.setAttribute("id", "surnlbl");
        var text = document.createTextNode("Surname");
        label.appendChild(text);

        div.appendChild(label);

        // input
        var input = document.createElement("INPUT");
        input.setAttribute("class", "form-control");
        input.setAttribute("placeholder", "Enter surname");
        input.setAttribute("id", "surname");
        input.setAttribute("required", "");

        div.appendChild(input);

        // small
        var small = document.createElement("SMALL");
        small.setAttribute("class", "error-msg text-danger");
        small.setAttribute("id", "surnameErrorMsg");
        small.setAttribute("hidden", "");
        text = document.createTextNode("Invalid surname, please enter only alphabetical characters");
        small.appendChild(text);

        div.appendChild(small);
}

function FleetManager()
{
    // Requires Fleet Manager Name
        //<div class="form-group" id="fleetMgrName">
        //    <label for="firstname">First name</label>
        //    <input type="text" class="form-control" id="firstname" placeholder="Enter first name" required>
        //    <small id="firstNameErrorMsg" class="error-msg text-danger" hidden>Invalid name, please enter only alphabetical characters</small>
        //</div>
    var div = document.createElement("div");
    div.setAttribute("class", "form-group");
    div.setAttribute("id", "surnDiv");

    var element = document.getElementById("firstN");
    element.appendChild(div);

    // label
    var label = document.createElement("LABEL");
    label.setAttribute("for", "surname");
    label.setAttribute("id", "surnlbl");
    var text = document.createTextNode("Fleet Manager name");
    label.appendChild(text);

    div.appendChild(label);

    // input
    var input = document.createElement("INPUT");
    input.setAttribute("class", "form-control");
    input.setAttribute("placeholder", "Enter fleet manager name");
    input.setAttribute("id", "surname");
    input.setAttribute("required", "");

    div.appendChild(input);

    // small
    var small = document.createElement("SMALL");
    small.setAttribute("class", "error-msg text-danger");
    small.setAttribute("id", "surnameErrorMsg");
    small.setAttribute("hidden", "");
    text = document.createTextNode("Invalid fleet manager name, please enter only alphabetical characters");
    small.appendChild(text);

    div.appendChild(small);
}

function generateRandomPassword(length) // You can decide the length of the random generated password
{
    length = 6;
    var result = "";
    var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++)
    {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }

    $("#password").val(result);
    //document.getElementById("password").text = result;

    return result;
}

function toggleVisiblePassword()
{
    var pswInput = document.getElementById("password");
    if (pswInput.type === "password")
    {
        pswInput.type = "text";
    } else
    {
        pswInput.type = "password";
    }
}