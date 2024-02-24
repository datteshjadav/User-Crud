$(document).ready(function () {
    getStudents();
    hideAlerts();


    //Hiding alert
    function hideAlerts() {
        $('#messageSuccess').hide();
        $('#messageFail').hide();

    }
    function successMsg(str) {
        $("#messageSuccess").text('');
        $("#messageSuccess").append(str);
        $("#messageSuccess").show().delay(3000).fadeOut();
    }
    function alertMsg(str) {
        $("#messageFail").append(str);
        $("#messageFail").show().delay(3000).fadeOut();
    }

    //Clearing values
    function clearValues(){
        $('#studname,#studage,input[name="studgender"],#studphone,input[name="studlanguage"],#DropdownListArea,#studAddress,#EditStudentId,#editstudname,#editstudage,input[name="editstudgender"],#editstudphone,input[name="editstudlanguage"],#EditDropdownListArea,#EditStudAddress').val('');
    }

    //Get DropDown Values
    function getDropdownValues() {
        var dropdown = $("#DropdownListArea , #EditDropdownListArea");
        dropdown.empty();
        $.ajax({
            url: '/StudentAjax/GetDropdownValue',
            type: 'GET',
            dataType: 'json',
            timeout: 0,
            success: function (data) {
                data.forEach((course) => {
                    // console.log(book.bid);
                    var row = '<option class="dropdown-item" value="' + course + '">' + course + '</option>';
                    dropdown.append(row);
                });
            }
        });
    }

    //geting student details
    function getStudents() {
        $.ajax({
            url: '/StudentAjax/GetStudents',
            type: 'GET',
            dataType: 'json',
            timeout: 0,
            success: function (studentList) {
                // console.log(studentList);
                var tableContent = $('#TableContent');
                tableContent.empty();
                studentList.forEach(student => {
                    var row = '<tr>';
                    row += '<td>' + student.c_studid + '</td>';
                    row += '<td>' + student.c_studname + '</td>';
                    row += '<td>' + student.c_studage + '</td>';
                    row += '<td>' + student.c_studphone + '</td>';
                    row += '<td>' + student.c_studcourse + '</td>';
                    row += '<td>' + student.c_studaddress + '</td>';
                    row += '<td>';
                    row += '<div class="d-flex justify-content-between">';
                    row += '<button type="button" id="EditBtn" class="btn btn-outline-success" data-id="' + student.c_studid + '" data-bs-target="#EditStudentModel" data-bs-toggle="modal"><i class="bi bi-pencil-square"></i>Edit</button>';
                    row += '<button type="button" id="DeleteBtn" class="btn btn-outline-danger" data-id="' + student.c_studid + '"><i class="bi bi-trash"></i>Delete</button>';
                    row += '</div>';
                    row += '</td>';
                    row += '</tr>';
                    tableContent.append(row);
                });

            }
        });
    }

    $('#AddStudentModelBtn').on('click',function(){
        getDropdownValues();
    });

    //Adding Student
    $('#printbtn').on('click', function () {
        AddStudent();
        getStudents();
    });
    function AddStudent() {
        var student = {
            c_studid: 0,
            c_studname: $('#studname').val(),
            c_studage: parseInt($('#studage').val()),
            c_studphone: parseInt($('#studphone').val()),
            c_studcourse: $("#DropdownListArea").val(),
            c_studaddress: $('#studAddress').val(),
        }
        // Debug
        console.log(student);

        $.ajax({
            url: '/StudentAjax/addStudent',
            type: 'POST',
            data: student,
            dataType: 'json',
        }).done((data) => {
            successMsg(data.message);
            getStudents();
            clearValues();
        });

    }

    //Edit Student
    $(document).on('click', '#EditBtn', function () {
        
        var id = $(this).data('id');
        // console.log(id);

        //getting the student values
        $.ajax({
            url: '/StudentAjax/GetStudentDetails',
            method: 'GET',
            data: {id:id},
            contentType: 'application/json',
            timeout: 0
        }).done((student) => {
            console.log(student);
            $('#EditStudentId').attr('data-id', id);
            $('#editstudname').val(student.c_studname);
            $('#editstudage').val(student.c_studage);
            $('#editstudphone').val(parseInt(student.c_studphone));
            $("#EditDropdownListArea").val(student.c_studcourse);
            $('#EditStudAddress').val(student.c_studaddress);
            getDropdownValues();
        });
    });
    //getting updated values
    $('#FinalEditBtn').on('click',function(){
        var student = {
            c_studid: parseInt($('#EditStudentId').attr('data-id')),
            c_studname: $('#editstudname').val(),
            c_studage: parseInt($('#editstudage').val()),
            c_studgender: $('input[name="editstudgender"]:checked').val(),
            c_studphone: $('#editstudphone').val(),
            c_studlanguage: $('input[name="editstudlanguage"]:checked').map(function () { return this.value; }).get(),
            c_studcoursename: $("#EditDropdownListArea").val(),
            c_studaddress: $('#EditStudAddress').val()
        }
        console.log(student);
        $.ajax({
            url: 'https://localhost:7093/api/StudentApi/UpdateStudent',
            type: 'PUT',
            data: JSON.stringify(student),
            contentType: 'application/json',
            timeout: 0,
            success: function (data) {
                getStudents();
                clearValues();
                successMsg(data.message);
            }

        });
    });

    // Delete Student
    $(document).on('click','#DeleteBtn', function () {
        var id = $(this).attr('data-id');
        $.ajax({
            url:'https://localhost:7093/api/StudentApi/DeleteStudent?id='+id,
            type: 'DELETE',
            timeout: 0,
            success: function(data){
                getStudents();
                alertMsg(data.message);
            }
        });

    });

});