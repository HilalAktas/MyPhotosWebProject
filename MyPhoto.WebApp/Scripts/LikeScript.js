$(function () {

    var photoids = [];
    //each ilk parametresi index, ikincisi elementtir.
    $("div[data-photo-id]").each(function (i, e) {

        photoids.push($(e).data("photo-id"));

    });

    console.log(photoids)



    $.ajax({
        method: "POST",
        url: "/Photo/GetLiked",
        data: { ids: photoids }


    }).done(function (data) {



        console.log(data);

        if (data.result != null && data.result.length > 0) {

            for (var i = 0; i <= data.result.length; i++) {

                var id = data.result[i];

                var likedPhoto = $("div[data-photo-id=" + id + "]");

                var btn = likedPhoto.find("button[data-liked]");

                var span = btn.children().first();

                btn.data("liked", true);

                span.removeClass("glyphicon-heart-empty");

                span.addClass("glyphicon-heart");



            }

        }


    }).fail(function () {

    });

    $("button[data-liked]").click(function () {

        var btn = $(this);
        var liked = btn.data("liked");
        var photoid = btn.data("photo-id");
        var spanHeart = btn.find("span.like-heart");
        var spanCount = btn.find("span.like-count");
        console.log(liked);
        console.log("like count : " + spanCount.text());
        $.ajax({

            method: "POST",
            url: "/Photo/SetLikeState",
            //Yukarıdan true gelmişse false, false gelmişse trueya çeviriyorum.Liked, unliked
            data: { "photoid": photoid, "liked": !liked }


        }).done(function (data) {
            console.log(data);

            if (data.hasError) {
                alert(data.errorMessage);
            } else {
                liked = !liked;
                btn.data("liked", liked);
                spanCount.text(data.result);

                console.log("like count(after) : " + spanCount.text());


                spanStar.removeClass("glyphicon-heart-empty");
                spanStar.removeClass("glyphicon-heart");
                if (liked) {
                    spanStar.addClass("glyphicon-heart");
                } else {
                    spanStar.addClass("glyphicon-heart-empty");
                }

            }


        }).fail(function () {

            alert("Sunucu ile bağlantı kurulamadı.");

        })

    });
});
