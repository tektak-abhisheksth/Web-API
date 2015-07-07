ZeroClipboard.config({ swfPath: "/Assets/ZeroClipboard.swf" });

function setNewCopy(arr) {
    if (arr.get(0) !== undefined) {
        var client = new ZeroClipboard(arr.find("[name=copy-button]"));
        client.on("copy", function (event) {
            var copytext = $(event.target).closest('div').find('pre').text();
            client.setData("text/plain", copytext);
            client.setData("text/html", copytext);
        });
    }
}

function setupCopyclient(t) {
    var client = new ZeroClipboard(t);
    setCopy(client);
};

function setCopy(client) {
    client.on("ready", function () {
        client.on("copy", function (event) {
            var copytext = $(event.target).closest('div').find('pre').text();;
            client.setData("text/plain", copytext);
            client.setData("text/html", copytext);
        });
    });
}

function initializeToolTip() {
    var template = '<div class="popover"><div class="arrow"></div><h4 class="popover-title"></h4><div class="popover-content"></div></div>';
    $("[data-toggle='popover']").popover({ placement: "top", title: "Description", template: template });
}

function animIn(elem, needAnim, persist, clr) {
    $(elem).stop();
    if (needAnim) {
        $(elem).slideDown(200).delay(250).effect("pulsate", { times: 2 }, 1500).delay(100).animate({ backgroundColor: clr }, 500);
        if (!persist)
            $(elem).animate({ backgroundColor: "#FFFFFF" }, 500);
    } else $(elem).show();
}

function animOut(elem) {
    $(elem).stop().css("backgroundColor", "#FFFFFF");
    if ($(elem).is(":visible"))
        $(elem).effect("transfer", { to: $("#closeAll"), className: "dotMatrix" }, 1000);
    $(elem).hide();
}