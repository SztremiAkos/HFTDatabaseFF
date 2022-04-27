let teachers = [];

fetch('http://localhost:6157/teacher')
    .then(x => x.json())
    .then(y => {
        teachers = y
        console.log(teachers)
        display();
    })



function display() {
    teachers.forEach(t => {
        document.getElementById("resultarea").innerHTML +=
            "<tr><td>" + t.teacherId + "</td><td>"
            + t.firstname + "</td><td>"
            + t.lastName + "</td><td>"
            + t.age + "</td><td>"
                + t.salary+ "</td></tr>"

        console.log(t.salary);
    })
}