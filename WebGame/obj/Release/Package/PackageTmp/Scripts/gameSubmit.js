﻿$(document).ready(function () {
    $('#exampleModal').on('shown.bs.modal', function () {
        $('.FullName').focus();
    });

    $('#gameSubmit').click(function (e) {
        var formData = new FormData();
        formData.append('FullName', $('.FullName').val());
        formData.append('Email', $('.Email').val());
        formData.append('Title', $('.Title').val());
        formData.append('FootageLink', $('.FootageLink').val());
        formData.append('Featured', $('#Featured').val());
        formData.append('CompanyName', $('.CompanyName').val());
        formData.append('Country', $('.Country').val());
        formData.append('LinkStore', $('.LinkStore').val());
        formData.append('MoreAbout', $('.MoreAbout').val());
        $.ajax({
            url: '/Home/SubmitGame',
            data: formData,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (data) {
                $("#exampleModal .close").click()
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    })
});