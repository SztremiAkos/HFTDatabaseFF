let teachers = [];
let connection = null;

let teacherIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:6157/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    connection.on
        (
            "TeacherCreated", (user, message) => {
                getdata();
            });
    connection.on
        (
            "TeacherDeleted", (user, message) => {
                getdata();
            });
    connection.on
        (
            "TeacherUpdated", (user, message) => {
                getdata();
            });
    connection.onclose
        (async () => {
            await start();
        });
    start();


}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:6157/teacher')
        .then(x => x.json())
        .then(y => {
            teachers = y
            //console.log(teachers)
            display();
        })
}

function display() {
    document.getElementById("resultarea").innerHTML = null;
    teachers.forEach(t => {
        document.getElementById("resultarea").innerHTML +=
            "<tr><td>" + t.teacherId + "</td><td>"
            + t.firstname + "</td><td>"
            + t.lastName + "</td><td>"
            + t.age + "</td><td>"
            + t.salary + "</td><td>"
            + `<button type="button" onclick="remove(${t.teacherId})">Remove</button>`
            + `<button type="button" onclick="showupdate(${t.teacherId})">Update</button>` + "</td></tr>"
    });

}

function showupdate(id) {
    teacherIdToUpdate = id;
    document.getElementById("tFNameup").value = teachers.find(x => x['teacherId'] == id)['firstname'];
    document.getElementById("tLNameup").value = teachers.find(x => x['teacherId'] == id)['lastName'];
    document.getElementById("tAgeup").value = teachers.find(x => x['teacherId'] == id)['age'];
    document.getElementById("tSalaryup").value = teachers.find(x => x['teacherId'] == id)['salary'];
    document.getElementById("updateformdiv").style.display = 'flex';


}
function update() {
    document.getElementById("updateformdiv").style.display = 'none';
    let fname = document.getElementById('tFNameup').value;
    let lname = document.getElementById('tLNameup').value;
    let tage = document.getElementById('tAgeup').value;
    let tsalary = document.getElementById('tSalaryup').value;
    fetch('http://localhost:6157/teacher', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                teacherId: teacherIdToUpdate,
                firstname: fname,
                lastName: lname,
                age: tage,
                salary: tsalary
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function remove(id) {
    fetch('http://localhost:6157/teacher/' + id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function create() {
    let fname = document.getElementById('tFName').value;
    let lname = document.getElementById('tLName').value;
    let tage = document.getElementById('tAge').value;
    let tsalary = document.getElementById('tSalary').value;

    fetch('http://localhost:6157/teacher', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                firstname: fname,
                lastName: lname,
                age: tage,
                salary: tsalary
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}
