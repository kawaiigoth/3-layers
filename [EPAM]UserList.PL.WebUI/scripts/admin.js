/// <reference path="jquery-2.2.3.js" />
(function () {




    $("body").on("click", '.remove-rights', RemoveRights);
    $("body").on("click", '.give-rights', GiveRights);


    function GiveRights() {
        var btn = $(this);
        var user = $(this).parent().data("name");
        $.ajax({
            url: '/Actions/admin/AdminAction',
            type: 'post',
            data: {
                obj: 'give_rights',
                username: user,
            }
        })
        .success(function (data) {
            console.log(btn);
            btn.text("Remove rights");
            btn.removeClass("give-rights").addClass("remove-rights");
            btn.parent().siblings(".is-moderator").eq(0).text("True");
        })
    }

    function RemoveRights() {
        var user = $(this).parent().data("name");
        var btn = $(this);
        $.ajax({
            url: '/Actions/admin/AdminAction',
            type: 'post',
            data: {
                obj: 'remove_rights',
                username: user,
            }
        })
        .success(function (data) {
            console.log(btn);
            btn.text("Give rights");
            btn.removeClass("remove-rights").addClass("give-rights");
            btn.parent().siblings(".is-moderator").eq(0).text("False");
        })
    }

}())