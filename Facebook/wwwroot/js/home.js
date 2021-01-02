src = "https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"
src = "~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"

//function RefreshHome() {
//    $("PostPartial").load("/Facebook/Home/");
//    $.ajax({
//        type: "GET",
//        url: "/Facebook/Home",
        
//    })
    
//}

$(document).ready(function () {
    $("#PostPartial").load("/Facebook/GetPosts");
});


function Post() {
    let txt = $("#PostText").val();
    let img = $("#PostImage").val();
    let Post = {
        Text: txt,
        Image:img
    };
    $.ajax({
        type: "POST",
        url: "/Facebook/Post",
        data: Post,
        //dataType :"Json",
        success: function (response) {
            if (response) {
                $("#PostPartial").html(response);
                
                console.log("İşlem Başarılı");
            }
            else {
                alert("Hata");
               
            }
        },
        error: function () {

            console.log("Hata Oluştu");
        }
    });
}

function ProfileSearch() {
    let profile = $("#SearchProfile").val();
    //let obj = HttpContext.Session.GetObject("User");

    let user = {
        Name: profile,
    };

    $.ajax({
        type: "POST",
        url: "/Profile/GetFriendProfile",
        data: user,
        //dataType :"Json",
        success: function (response) {
            if (response) {
                location.href="/Profile/Profile"
                console.log("İşlem Başarılı");
            }
            else {
                debugger;
                alert("Hata");
                location.href = "/Facebook/Home"
            }

        },
        error: function () {

            console.log("Hata Oluştu");
        }
    });
}