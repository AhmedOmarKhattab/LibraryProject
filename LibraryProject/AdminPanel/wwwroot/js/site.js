function PrievewImage() {
    var Input = document.getElementById("ImageSource");
    var Preview = document.getElementById("BookImage");
    var url = document.getElementById("url");
    var file = Input.files[0];
    var reader = new FileReader();
    reader.onload = function (e) {
        Preview.src = e.target.result;
        url.value = "photo added succesfully";
    };
    reader.readAsDataURL(file);
}
