let index = 0;

function AddTag() {
    var tagEntry = document.getElementById("TagEntry");

    //Use Search function to help detect an error
    let searchResult = Search(tagEntry.value);

    if (searchResult != null) {
        //Trigger Sweetalert
        SwalWithDarkButton.fire({
            html: `<span class='font-weight-bolder'>${searchResult.toUpperCase()}</span>`,
            title: 'Error!',
            icon: 'error',
        })

        console.log(searchResult)
    }
    else {
        //Create a new select option
        let newOption = new Option(tagEntry.value, tagEntry.value)
        document.getElementById("TagList").options[index++] = newOption;
    }

    //Clear tag entry control
    tagEntry.value = "";

    return true;
}

function DeleteTag() {
    let tagCount = 1;
    let tagList = document.getElementById("TagList");
    if (!tagList) return false;

    if (tagList.selectedIndex == -1) {
        SwalWithDarkButton.fire({
            html: `<span class='font-weight-bolder'>Choose a Tag before Deleting!</span>`,
            title: 'Error!',
            icon: 'error',
        });

        return true;
    }

    while (tagCount > 0) {
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount;
        }
        else {
            tagCount = 0;
        index--;
        }
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected")
})

//Look for TagValues
if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        //Load up or replace options
        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}

//Search Function for Empty/Duplicate Tag
function Search(str) {
    if (str == "") {
        return 'Empty Tags are not permitted!'
    }

    var tagsElement = document.getElementById("TagList");

    if (tagsElement) {
        let options = tagsElement.options;

        for (let i = 0; i < options.length; i++) {
            if (options[i].value == str) {
                return `The Tag #${str} was detected as a Duplicate and are not permitted!`
            }
        }
    }
}

const SwalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger btn-sm btn-block btn-outline-dark'
    },
    timer: 3500,
    buttonsStyling: false
})