
var REST_DATA = 'api/db';
var KEY_ENTER = 13;

function addItem(item, isNew) {
    alert("Hi");
    var row = document.createElement('tr');
    var id = item && item._id;
    var rev = item && item._rev;
    if (id) {
        row.setAttribute('data-id', id);
        row.setAttribute('data-rev', rev);
    }
    row.innerHTML = "<td style='width:90%'><textarea onchange='saveChange(this)' onkeydown='onKey(event)'></textarea></td>" +
        "<td class='deleteBtn' onclick='deleteItem(this)' title='delete me'></td>";
    var table = document.getElementById('notes');
    console.log(table.lastChild);
    table.lastChild.appendChild(row);
    var textarea = row.firstChild.firstChild;
    if (item) {
        textarea.value = item.text;
    }
    row.isNew = !item || isNew;
    textarea.focus();
}

function saveChange(contentNode, callback) {
    debugger;
    var row = contentNode.parentNode.parentNode;
    var id = row.getAttribute('data-id');
    var rev = row.getAttribute('data-rev');
    var DeleiveryAddress = document.getElementById('DeleiveryAddress');
    var DeleiveryPinCode = document.getElementById('DeleiveryPinCode');
    var ParcelLocation = document.getElementById('ParcelLocation');
    var ParcelLocationPinCode = document.getElementById('ParcelLocationPinCode');
    var DateofDelivery = document.getElementById('DateofDelivery');

    var PickUpDate = document.getElementById('PickUpDate');
    var DeliveryCharge = document.getElementById('DeliveryCharge');
   
    id = id == null ? "" : "DeleiveryAddress";
    var data = { Array:[
        {
            id: id,
            rev: rev,
            text: DeleiveryAddress.value
        },
        {
            id: "DeleiveryPinCode",
            rev: rev,
            text: DeleiveryPinCode.value
        },
        {
            id: "ParcelLocation",
            rev: rev,
            text: ParcelLocation.value
        },
        {
            id: "ParcelLocationPinCode",
            rev: rev,
            text: ParcelLocationPinCode.value
        },
        {
            id: "DateofDelivery",
            rev: rev,
            text: DateofDelivery.value
        },
        {
            id: "PickUpDate",
            rev: rev,
            text: PickUpDate.value
        },
        {
            id: "DeliveryCharge",
            rev: rev,
            text: DeliveryCharge.value
        }

    ]
    };
    if (row.isNew) {
        delete row.isNew;
        xhrPost(REST_DATA, data, function (item) {
            row.setAttribute('data-id', item.id);
            row.setAttribute('data-rev', item.rev);
            callback && callback();
        }, function (err) {
            console.error(err);
        });
    } else {
        data.id = row.getAttribute('data-id');
        data.rev = row.getAttribute('data-rev');
        xhrPut(REST_DATA, data, function () {
            console.log('updated: ', data);
        }, function (err) {
            console.error(err);
        });
    }
}


function loadItems() {
    debugger;
    xhrGet(REST_DATA, function (data) {
        document.getElementById("loading").innerHTML = "";
        var receivedItems = data.rows || [];
        var items = [];
        var i;
        // Make sure the received items have correct format
        for (i = 0; i < receivedItems.length; ++i) {
            var item = receivedItems[i].doc;
            if (item && '_id' in item && 'text' in item) {
                items.push(item);
            }
        }
        for (i = 0; i < items.length; ++i) {
            addItem(items[i], false);
        }
    }, function (err) {
        console.error(err);
        document.getElementById("loading").innerHTML = "ERROR";
    });
}
//loadItems();

