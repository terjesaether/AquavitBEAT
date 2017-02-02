var loadFile = function (event) {
    var output = document.getElementById('output');
    output.src = URL.createObjectURL(event.target.files[0]);
    //$('.file-upload-text').text("");
};

var inputs = document.querySelectorAll('.inputfile');
Array.prototype.forEach.call(inputs, function (input) {
   //var label = input.nextElementSibling,
    var label = findElementById('file-upload-text'),
    //var label = document.getElementsByClassName('file-upload-text'),
        labelVal = label.innerHTML;

    input.addEventListener('change', function (e) {
        var fileName = '';

        if (this.files && this.files.length > 1)
            fileName = (this.getAttribute('data-multiple-caption') || '').replace('{count}', this.files.length);
        else
            fileName = e.target.value.split('\\').pop();

        if (fileName)
            label.querySelector('span').innerHTML = fileName;
        else
            label.innerHTML = labelVal;
    });
});