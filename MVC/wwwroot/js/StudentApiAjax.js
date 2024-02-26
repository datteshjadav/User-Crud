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
    function clearValues() {
        $('#studname,#studage,input[name="studgender"],#studphone,input[name="studlanguage"],#DropdownListArea,#studAddress,#EditStudentId,#editstudname,#editstudage,input[name="editstudgender"],#editstudphone,input[name="editstudlanguage"],#EditDropdownListArea,#EditStudAddress').val('');
    }
    //Get DropDown Values
    function getDropdownValues() {
        var dropdown = $("#DropdownListArea , #EditDropdownListArea");
        dropdown.empty();
        $.ajax({
            url: 'https://localhost:7051/api/StudentApi',
            type: 'GET',
            // dataType: 'json',
            timeout: 0,
            contentType: "application/json",
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
            url: 'https://localhost:7051/api/StudentApi/getall',
            type: 'GET',
            dataType: 'json',
            timeout: 0,
            headers: {
                "Authorization": 'Bearer ' + localStorage.getItem('token')
            },
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

    $('#AddStudentModelBtn').on('click', function () {
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
            url: 'https://localhost:7051/api/StudentApi/AddStudent',
            type: 'POST',
            data: JSON.stringify(student),
            // dataType: 'json',
            contentType: "application/json",
            headers: {
                "Authorization": 'Bearer ' + localStorage.getItem('token')
            },
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
            url: 'https://localhost:7051/api/StudentApi/Getstudentbyid?id=' + id,
            method: 'GET',
            data: { id: id },
            dataType: "json",
            headers: {
                "Authorization": 'Bearer ' + localStorage.getItem('token')
            },
            timeout: 0
        }).done((student) => {
            console.log(student);
            getDropdownValues();
            $('#EditStudentId').attr('data-id', id);
            $('#editstudname').val(student.c_studname);
            $('#editstudage').val(student.c_studage);
            $('#editstudphone').val(parseInt(student.c_studphone));
            $("#EditDropdownListArea").val(student.c_studcourse);
            $('#EditStudAddress').val(student.c_studaddress);
        });
    });
    //getting updated values
    $('#FinalEditBtn').on('click', function () {
        var student = {
            c_studid: parseInt($('#EditStudentId').attr('data-id')),
            c_studname: $('#editstudname').val(),
            c_studage: parseInt($('#editstudage').val()),
            c_studphone: $('#editstudphone').val(),
            c_studcourse: $("#EditDropdownListArea").val(),
            c_studaddress: $('#EditStudAddress').val()
        }
        console.log(student);
        $.ajax({
            url: 'https://localhost:7051/api/StudentApi/UpdateStudent',
            type: 'PUT',
            data: JSON.stringify(student),
            contentType: 'application/json',
            headers: {
                "Authorization": 'Bearer ' + localStorage.getItem('token')
            },
            // dataType: 'json',
            timeout: 0,
            success: function (data) {
                getStudents();
                clearValues();
                successMsg(data.message);
            }

        });
    });

    // Delete Student
    $(document).on('click', '#DeleteBtn', function () {
        var id = $(this).attr('data-id');
        $.ajax({
            url: 'https://localhost:7051/api/StudentApi/DeleteStudent/id/' + id,
            type: 'DELETE',
            headers: {
                "Authorization": 'Bearer ' + localStorage.getItem('token')
            },
            timeout: 0,
            success: function (data) {
                getStudents();
                alertMsg(data.message);
            }
        });

    });

});