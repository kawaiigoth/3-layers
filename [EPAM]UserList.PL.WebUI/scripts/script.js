/// <reference path="jquery-2.2.3.js" />
(function () {

    var popoverSettings = {
        selector: '[data-toggle="popover"]',
        html: true,
        trigger: 'hover',
        content: function () {
            return '<img src="' + $(this).data('img') + '" />';
        }
    }

    $('body').popover(popoverSettings);

    $("#TableOfUsers tbody").on("click", '.show-rewards', showRewards);
    $("#TableOfUsers tbody").on("click", '.edit-user-age', editUserAge);
    $("#TableOfUsers tbody").on("click", '.edit-user-name', editUserName);
    $("#TableOfUsers tbody").on("click", '.edit-user-photo', editUserPhoto);
    $("#TableOfUsers tbody").on("click", '.delete-user', deleteUserDialog);
    $(".container tbody").on("click", '.user-delete-reward', deleteReward);

    $("#AwardsTable tbody").on("click", '.show-users', showUsers);
    $("#AwardsTable tbody").on("click", '.edit-award-name', editAwardName);
    $("#AwardsTable tbody").on("click", '.edit-award-photo', editAwardPhoto);
    $("#AwardsTable tbody").on("click", '.delete-award', deleteAwardDialog);

    $(".create-user").click(function () {

        event.preventDefault();
        $.ajax({
            url: '/Actions/admin/CreateUser',
            type: 'post',
            data: {
                obj: 'show_modal',
            }
        })
        .success(function (data) {
            $(".container").append(data);
            $("#CreateUser").modal('show');
            $('.user-complete-creation').unbind("click");
            $('.user-complete-creation').click(createUser);
        })
    });

    $(".create-award").click(function () {

        event.preventDefault();
        $.ajax({
            url: '/Actions/admin/CreateAward',
            type: 'post',
            data: {
                obj: 'show_modal',
            }
        })
        .success(function (data) {
            $(".container").append(data);
            $("#CreateAward").modal('show');
            $('.award-complete-creation').unbind("click");
            $('.award-complete-creation').click(createAward);
        })
    });

    $("#LoginBtn").click(function (e) {
        e.preventDefault();

        var login_data = new FormData($('#LoginForm')[0]);
        console.log("data collected - sending");
        //user_data.append('name', $user_name.val());
        //user_data.append('bday', parsed_date);
        //user_data.append('type', 'users');
        $.ajax({
            url: '/Actions/user/Login',
            type: 'post',
            cache: false,
            contentType: false,
            processData: false,
            data: login_data,
        })
        .success(function (data) {
            console.log("sended");
            $('#LoginModal').modal('hide');
            data = JSON.parse(data);
            var success = data["success"];
            if (success == "ok") {
                console.log("ok");
            }
            else {
                console.log("fail");
            }
        })
    })

    $("#RegisterBtn").click(function (e) {
        e.preventDefault();

        var register_data = new FormData($('#RegisterForm')[0]);
        console.log("data collected - sending");
        //user_data.append('name', $user_name.val());
        //user_data.append('bday', parsed_date);
        //user_data.append('type', 'users');
        $.ajax({
            url: '/Actions/user/Register',
            type: 'post',
            cache: false,
            contentType: false,
            processData: false,
            data: register_data,
        })
        .success(function (data) {
            console.log("sended");
            $('#RegisterModal').modal('hide');
            data = JSON.parse(data);
            var success = data["success"];
            if (success == "ok") {
                console.log("ok");
            }
            else {
                console.log("fail");
            }
        })
    })

    function deleteUserDialog() {
        event.preventDefault();
        var id = $(this).parent().parent().attr("id");
        $.ajax({
            url: '/Actions/admin/EditUser',
            type: 'post',
            data: {
                obj: 'delete',
                ID: id,
            }
        })
        .success(function (data) {
            $("#DeleteUser").remove();
            $(".container").append(data);
            $("#DeleteUser").modal('show');
            $('.delete-user-confirm').unbind("click");
            $('.delete-user-confirm').click(deleteUserRequest);
        })
    }

    function deleteAwardDialog() {
        event.preventDefault();
        var id = $(this).parent().parent().attr("id");
        $.ajax({
            url: '/Actions/admin/EditAward',
            type: 'post',
            data: {
                obj: 'delete',
                ID: id,
            }
        })
        .success(function (data) {
            $("#DeleteAward").remove();
            $(".container").append(data);
            $("#DeleteAward").modal('show');
            $('.delete-award-confirm').unbind("click");
            $('.delete-award-confirm').click(deleteAwardRequest);
        })
    }

    function deleteUserRequest() {
        event.preventDefault();
        var user_data = new FormData($('#DeleteionID')[0]);
        user_data.append('obj', 'delete_request'),
        $.ajax({
            url: '/Actions/admin/EditUser',
            type: 'post',
            cache: false,
            contentType: false,
            processData: false,
            data: user_data

        })
        .success(function (data) {
            $("#DeleteUser").modal('hide');
            data = JSON.parse(data);
            var user_id = data["user_id"];
            $('#' + user_id).remove();
        })
    }

    function deleteAwardRequest() {
        event.preventDefault();
        var award_data = new FormData($('#ADeleteionID')[0]);
        award_data.append('obj', 'delete_request'),
        $.ajax({
            url: '/Actions/admin/EditAward',
            type: 'post',
            cache: false,
            contentType: false,
            processData: false,
            data: award_data

        })
        .success(function (data) {
            $("#DeleteAward").modal('hide');
            data = JSON.parse(data);
            var award_id = data["award_id"];
            $('#' + award_id).remove();
        })
    }

    function editUserAge() {
        event.preventDefault();
        var id = $(this).parent().parent().attr("id");
        $.ajax({
            url: '/Actions/admin/EditUser',
            type: 'post',
            data: {
                obj: 'edit_age_modal',
                ID: id,
            }
        })
        .success(function (data) {
            $("#ChangeUserAge").remove();
            $(".container").append(data);
            $("#ChangeUserAge").modal('show');
            $('.user-submit-age').unbind("click");
            $('.user-submit-age').click(ReceiveUserAge);
        })
    }

    function editUserName() {
        event.preventDefault();
        var id = $(this).parent().parent().attr("id");
        $.ajax({
            url: '/Actions/admin/EditUser',
            type: 'post',
            data: {
                obj: 'edit_name_modal',
                ID: id,
            }
        })
        .success(function (data) {
            $("#ChangeUserName").remove();
            $(".container").append(data);
            $("#ChangeUserName").modal('show');
            $('.user-submit-name').unbind("click");
            $('.user-submit-name').click(ReceiveUserName);
        })
    }

    function editAwardName() {
        event.preventDefault();
        var id = $(this).parent().parent().attr("id");
        $.ajax({
            url: '/Actions/admin/EditAward',
            type: 'post',
            data: {
                obj: 'edit_name_modal',
                ID: id,
            }
        })
        .success(function (data) {
            $("#ChangeAwardName").remove();
            $(".container").append(data);
            $("#ChangeAwardName").modal('show');
            $('.award-submit-name').unbind("click");
            $('.award-submit-name').click(ReceiveAwardName);
        })
    }

    function editUserPhoto() {
        event.preventDefault();
        var id = $(this).parent().parent().attr("id");
        $.ajax({
            url: '/Actions/admin/EditUser',
            type: 'post',
            data: {
                obj: 'edit_photo_modal',
                ID: id,
            }
        })
        .success(function (data) {
            $("#ChangeUserPhoto").remove();
            $(".container").append(data);
            $("#ChangeUserPhoto").modal('show');
            $('.user-submit-photo').unbind("click");
            $('.user-submit-photo').click(ReceiveUserPhoto);
        })
    }

    function editAwardPhoto() {
        event.preventDefault();
        var id = $(this).parent().parent().attr("id");
        $.ajax({
            url: '/Actions/admin/EditAward',
            type: 'post',
            data: {
                obj: 'edit_photo_modal',
                ID: id,
            }
        })
        .success(function (data) {
            $("#ChangeAwardPhoto").remove();
            $(".container").append(data);
            $("#ChangeAwardPhoto").modal('show');
            $('.award-submit-photo').unbind("click");
            $('.award-submit-photo').click(ReceiveAwardPhoto);
        })
    }

    function ReceiveUserAge() {
        event.preventDefault();
        var dd;
        var user_data = new FormData($('#UserAge')[0]);
        dd = user_data.get('BDay');
        user_data.set('BDay', parseDate(dd));
        user_data.append('obj', 'rec_age'),
        $.ajax({
            url: '/Actions/admin/EditUser',
            type: 'post',
            cache: false,
            contentType: false,
            processData: false,
            data: user_data

        })
        .success(function (data) {
            $("#ChangeUserAge").modal('hide');
            data = JSON.parse(data);
            var user_age = data["age"],
                id = data["id"];
            $('#' + id).find('.user-age').text(user_age);
        })

    }

    function ReceiveUserName() {
        event.preventDefault();
        var user_data = new FormData($('#UserName')[0]);
        user_data.append('obj', 'rec_name'),
        $.ajax({
            url: '/Actions/admin/EditUser',
            type: 'post',
            cache: false,
            contentType: false,
            processData: false,
            data: user_data

        })
        .success(function (data) {
            $("#ChangeUserName").modal('hide');           
            data = JSON.parse(data);
            var user_name = data["name"],
                id = data["id"];
            $('#' + id).find('.username').text(user_name);
        })
    }

    function ReceiveAwardName() {
        event.preventDefault();
        var award_data = new FormData($('#AwardName')[0]);
        award_data.append('obj', 'rec_name'),
        $.ajax({
            url: '/Actions/admin/EditAward',
            type: 'post',
            cache: false,
            contentType: false,
            processData: false,
            data: award_data

        })
        .success(function (data) {
            $("#ChangeAwardName").modal('hide');
            data = JSON.parse(data);
            var award_name = data["name"],
                id = data["id"];
            $('#' + id).find('.awardname').text(award_name);
        })
    }

    function ReceiveUserPhoto() {
        event.preventDefault();
        var user_data = new FormData($('#UserPhoto')[0]);
        user_data.append('obj', 'rec_photo'),
        $.ajax({
            url: '/Actions/admin/EditUser',
            type: 'post',
            cache: false,
            contentType: false,
            processData: false,
            data: user_data

        })
        .success(function (data) {
            $("#ChangeUserPhoto").modal('hide');           
            data = JSON.parse(data);
            var id = data["id"],
                html = data["html"];
            $('#' + id).find('.user-img').html(html);
            
        })
    }

    function ReceiveAwardPhoto() {
        event.preventDefault();
        var award_data = new FormData($('#AwardPhoto')[0]);
        award_data.append('obj', 'rec_photo'),
        $.ajax({
            url: '/Actions/admin/EditAward',
            type: 'post',
            cache: false,
            contentType: false,
            processData: false,
            data: award_data

        })
        .success(function (data) {
            $("#ChangeAwardPhoto").modal('hide');
            data = JSON.parse(data);
            var id = data["id"],
                html = data["html"];
            $('#' + id).find('.award-img').html(html);

        })
    }

    function parseDate(date) {
        date = date + '';
        date = date.split('-');
        var year = date[0],
            mounth = date[1],
            day = date[2];
        new_date = day + '-' + mounth + '-' + year;
        return new_date;
    }

    function createUser() {
        event.preventDefault();
        var dd;
        var user_data = new FormData($('#UserData')[0]);
        dd = user_data.get('BDay');
        user_data.set('BDay', parseDate(dd));
        user_data.append('obj', 'creation'),
        $.ajax({
            url: '/Actions/admin/CreateUser',
            type: 'post',
            cache: false,
            contentType: false,
            processData: false,
            data: user_data

        })
        .success(function (data) {
            $("#CreateUser").modal('hide');
            $('#TableOfUsers tbody').append(data);
        })
    }

    function createAward() {
        event.preventDefault();
        var award_data = new FormData($('#AwardData')[0]);
        award_data.append('obj', 'creation'),
        $.ajax({
            url: '/Actions/admin/CreateAward',
            type: 'post',
            cache: false,
            contentType: false,
            processData: false,
            data: award_data

        })
        .success(function (data) {
            $("#CreateAward").modal('hide');
            $('#AwardsTable tbody').append(data);
        })
    }

    function showRewards() {
        event.preventDefault();
        var id = $(this).parent().parent().attr("id");
        $.ajax({
            url: '/Actions/user/ShowRewards',
            type: 'post',
            data: {
                obj: 'rewards',
                ID: id,
            }
        })
        .success(function (data) {
            $("#ShowRewards").remove();
            $(".container").append(data);
            $('.user-delete-reward').click(deleteReward);
            $('.submit-award').click(submitAward);
            $("#ShowRewards").modal('show');
        })
    }

    function showUsers() {
        event.preventDefault();
        var id = $(this).parent().parent().attr("id");
        $.ajax({
            url: '/Actions/admin/ShowUsers',
            type: 'post',
            data: {
                obj: 'users',
                ID: id,
            }
        })
        .success(function (data) {
            $("#ShowUsers").remove();
            $(".container").append(data);
            $("#ShowUsers").modal('show');
        })
    }

    function submitAward() {

        event.preventDefault();
        var user_data = new FormData($('#AvalibleAwards')[0]),
            id = $(this).attr("data-user-id");

        user_data.append('obj', 'selection');
        user_data.append('ID', id);
        $.ajax({
            url: '/Actions/user/ShowRewards',
            type: 'post',
            cache: false,
            contentType: false,
            processData: false,
            data: user_data,
        })
        .success(function (data) {
            $('#TableOfAwards tbody').append(data);
        })
    }

    function deleteReward() {

        event.preventDefault();
        var id = $(this).parent().parent().attr("id"),
            user_id = $(this).attr("data-user-id");
        $.ajax({
            url: '/Actions/user/ShowRewards',
            type: 'post',
            data: {
                obj: 'remove_reward',
                aID: id,
                uID: user_id,
            }
        })
        .success(function (data) {
            data = JSON.parse(data);
            var award_id = data["award_id"];
            $('#' + award_id).remove();
        })
    }

}())