//var date = new Date();
//document.getElementById('curr-year').innerHTML = date.getFullYear()

//src = "https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"
//src = "~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"


//PostPartialView --> boş div idsi
$(document).ready(function () {
    $("#ProfilePostPartial").load("/Profile/Get"); 
    $("#PicturePartial").load("/Profile/GetImage"); 
});


//PostText -> index viewinde 
function ProfilePost() {
    let txt = $("#ProfilePostText").val();
    let image = $("#ProfilePostImage").val();
    let Post = {
        Text: txt,
        Image: image
    };
    $.ajax({
        type: "POST",
        url: "/Profile/ProfilePost",
        data: Post,
        //dataType :"Json",
        success: function (response) {
            if (response) {
                $("#ProfilePostPartial").html(response);

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