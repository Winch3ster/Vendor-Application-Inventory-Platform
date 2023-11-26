var unreadNotification = [];
var readNotification = [];
var currentUser = null;

function GetCurrentUser() {

    fetch('https://localhost:7050/api/CurrentUser/')
        .then(response => {
            console.log(response)
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {

            currentUser = data;
            console.log(data);

            console.log(currentUser)
            console.log(currentUser.employeeID)
            GetNotification()
            })
        .catch(error => console.error('Error:', error));
}

function SetLastRetriveChangeLogForUser() {

    console.log("SetLastRetriveChangeLogForUser is running")

    // Make a PATCH request to the API endpoint
    fetch("https://localhost:7050/api/CurrentUser/", {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json',
        },
        })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.text();
        })
        .then(data => {
            // Handle the successful response data
            console.log('API response:', data);
        })
        .catch(error => {
            // Handle errors
            console.error('Error calling API:', error);
        });

}

function ShowNotification() {
    RenderNotificationMessage()
    SetLastRetriveChangeLogForUser()
    let notificationbox = document.getElementById('notification-message-box');
    notificationbox.style.display = 'block';

    MoveNotificationToRead()
}

function CloseNotification() {
    let notificationbox = document.getElementById('notification-message-box');
    notificationbox.style.display = 'none';
    RemoveAllMessageMark();
    ClearNotificationMark()
}

function GetNotification() {
        
    // Fetch data from the API
    fetch(`https://localhost:7050/api/ChangeLog?userid=${currentUser.employeeID}`)
        .then(response => {
            console.log(response)
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {


                // Handle the data
                data.forEach(item => {

                   
                    switch (item.actionPerformed) {
                        case 0:
                            action = "added"
                            break;
                        case 1:
                            action = "edited"
                            break;
                        case 2:
                            action = "deleted"
                            break;
                        default:
                            action = "changed"
                    }

                    unreadNotification.push({
                        id: item.id,
                        entity: item.entityName,
                        action: action,
                        time: item.time.substring(0, 10)
                    });

                    console.log(`Unread notification: ${unreadNotification}`)

                    console.log(unreadNotification.length)
                    console.log("Get notification is running")
                    renderNotificationMark()
                });
            })
        .catch(error => console.error('Error:', error));

}

function renderNotificationMark() {
    var mark = document.getElementById('notification-icon');
    console.log("render mark is running")
    console.log(unreadNotification.length)
    console.log(mark)

    mark.style.display = 'block'
    mark.innerHTML = `${unreadNotification.length}`;
    
}
function ClearNotificationMark() {
    var mark = document.getElementById('notification-icon');


    mark.style.display = 'none'
    mark.innerHTML = "";


    console.log("readNotification:", readNotification);
    console.log("unreadNotification:", unreadNotification);
}
function MoveNotificationToRead() {

    // Move all data from unreadNotification to readNotification
    readNotification = readNotification.concat(unreadNotification);

    // Clear unreadNotification
    unreadNotification = [];
}

function RenderNotificationMessage() {
    var messagePanel = document.getElementById('notification-message-panel');


    unreadNotification.forEach(element => {

        var messageBlock = document.createElement("div");
        messageBlock.classList.add('notification-messageBlock');
        var indicator = document.createElement("div");
        indicator.classList.add('notification-indicator')

        var messageSpace = document.createElement("div");

        var span = document.createElement('span')
        span.innerHTML = `${element.entity} was ${element.action}`

        var p = document.createElement('p')
        p.style.color = '#4688F1'
        p.innerHTML = `${element.time}`


        messageSpace.appendChild(span)
        messageSpace.appendChild(p)
        messageBlock.appendChild(indicator)
        messageBlock.appendChild(messageSpace)

        messagePanel.appendChild(messageBlock)
    })
    //create message block
}

function RemoveAllMessageMark() {
    // Get all elements with the class "indicator" within the parent div
    var indicators = document.querySelectorAll('.notification-indicator');

    // Loop through each indicator and set display to none
    indicators.forEach(function (indicator) {
        indicator.style.display = 'none';
    });
}
GetCurrentUser();
