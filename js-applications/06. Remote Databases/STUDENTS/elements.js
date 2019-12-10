function getTdWithText(text) {
    let td = document.createElement('td');
    td.innerHTML = text;

    return td;
}

export function displayStudents(students) {
    let frag = document.createDocumentFragment();
    for (const stdnt of students) {
        let tr = document.createElement('tr');
        tr.appendChild(getTdWithText(stdnt.ID));
        tr.appendChild(getTdWithText(stdnt.FirstName));
        tr.appendChild(getTdWithText(stdnt.LastName));
        tr.appendChild(getTdWithText(stdnt.FacultyNumber));
        tr.appendChild(getTdWithText(stdnt.Grade));
        frag.appendChild(tr);
    }

    document.getElementById('table-body').innerHTML = '';
    document.getElementById('table-body').appendChild(frag);
}

export function getStudentInfo() {
    let student = {
        FirstName: document.getElementById('firstName').value,
        LastName: document.getElementById('lastName').value,
        FacultyNumber: document.getElementById('facultyNumber').value,
        Grade: Number(document.getElementById('grade').value),
    }

    if (Object.values(student).some(x => !x)) {
        document.getElementById('validation').innerHTML = 'You have to fill all the fields!';
        return undefined;
    }

    return student;
}

export function getNextId(students) {
    if (students.length > 0) {
        let lastId = students.sort((a, b) => b.ID - a.ID)[0].ID;
        return lastId + 1;
    } else {
        return 0;
    }
}