$(function () {
    $("form").submit(async e => {
        e.preventDefault();

        const q = $("#search").val();

        var respons = await fetch('/Rate/Search?query=' + q);
        var d = await respons.json();
        var data = JSON.parse(d)

        const template = $("#template").html();
        let results = "";
        for (var item in data) {
            let row = template;
            for (key in data[item]) {
                row = row.replaceAll('{' + key + '}', data[item][key]);
                row = row.replaceAll('%7B' + key + '%7D', data[item][key]);
            }
            results += row;
        }

        $('tbody').html(results);

       
        var divError = $("#errorMsg");
        //if there is no results need to show error msg
        if (results == "") {
            //get the error msg
            const errorMsg = $("#templateNoResults").html();
            //if the div of the error is not appear allready
            if (divError[0] == undefined) {
                //add the error msg
                $("#warpper").append(errorMsg);
            }
        }
        //if there are results
        else {
            //make sure to delete the msg error if there is one
            if (divError[0] != undefined) {
                divError[0].remove()
            }
        }
        
            
        
    });
});
