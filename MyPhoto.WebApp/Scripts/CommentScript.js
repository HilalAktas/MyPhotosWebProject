$.noConflict();

var photoCommentBodyId = "#modal_comment_body";
var photoid = -1;
$(function () {

    $('#modal_comment').on('show.bs.modal', function (e) {

        var button = $(e.relatedTarget);
        photoid = button.data("photo-id");

        $('#modal_comment_body').load("/Comment/ShowPhotoComment/" + photoid)

    })

});

function doComment(btn, e, commentid, spanid) {
    //addclass,removeclass jquery ile gelen methodlar.Bunları  kullanmak için gelen butonu jquery nesnesine çevirmelisin.

    var button = $(btn);

    var mode = button.data("edit-mode");

    if (e === "edit_click") {

        //mode false
        if (!mode) {


            button.data("edit-mode", true);
            button.removeClass("btn-warning");
            button.addClass("btn-success");
            var btnSpan = button.find("span");
            btnSpan.removeClass("glyphicon-edit");
            btnSpan.addClass("glyphicon-ok");

            $(spanid).attr("contenteditable", true);
            $(spanid).focus();
        }

        else {
            button.data("edit-mode", false);
            button.addClass("btn-warning");
            button.removeClass("btn-success");
            var btnSpan = button.find("span");
            btnSpan.addClass("glyphicon-edit");
            btnSpan.removeClass("glyphicon-ok");

            $(spanid).attr("contenteditable", false);



            var txt = $(spanid).text();

            $.ajax({
                method: 'POST',
                url: '/Comment/Edit/' + commentid,
                data: { text: txt }
            }).done(function (data) {

                if (data.result) {
                    // yorumlar partial tekrar yüklenir..
                    $("#modal_comment_body").load("/Comment/ShowPhotoComment/" + photoid);
                }
                else {
                    alert("Yorum güncellenemedi.");
                }

            }).fail(function () {
                alert("Sunucu ile bağlantı kurulamadı.");
            });
        }

    }

    else if (e === "remove_click") {

        var dialog_res = confirm("Yorum silinsin mi?")

        if (!dialog_res) return false;

        $.ajax({
            method: 'Get',
            url: '/Comment/Delete/' + commentid

        }).done(function (data) {
            if (data.result) {
                // yorumlar partial tekrar yüklenir..
                $("#modal_comment_body").load("/Comment/ShowPhotoComment/" + photoid);



            }
            else {

                alert("Yorum silinemedi.");
            }

        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        });


    }
    else if (e === "new_click") {

        var text = $("#new_text_comment").val();


        $.ajax({
            method: 'Post',
            url: '/Comment/Create/' + commentid,
            data: { "text": text, "photoid": photoid }

        }).done(function (data) {
            if (data.result) {
                // yorumlar partial tekrar yüklenir..
                $("#modal_comment_body").load("/Comment/ShowPhotoComment/" + photoid);



            }
            else {

                alert("Yorum eklenemedi.");
            }

        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        });

    }


}