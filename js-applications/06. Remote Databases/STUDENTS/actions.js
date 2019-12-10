import { fetchOperations } from "../src/fetchData.js";
import { displayStudents, getStudentInfo, getNextId } from "./elements.js";

const username = 'guest';
const password = 'guest';
const appId = 'kid_SJ8WUOz3S';

const urlTemplates = {
    students: () => `https://baas.kinvey.com/appdata/${appId}/students`,
    student: (id) => `https://baas.kinvey.com/appdata/${appId}/students/` + id,
}

export const actions = {
    load: async () => {

        try {
            let students = await fetchOperations.get(urlTemplates.students(), username, password);
            displayStudents(students);
        }
        catch (er) {
            window.alert(er.message);
        }
    },
    submit: async () => {
        try {
            let newStudent = getStudentInfo();
            if (newStudent) {
                let students = await fetchOperations.get(urlTemplates.students(), username, password);
                let id = getNextId(students);
                newStudent.ID = id;
                await fetchOperations.post(urlTemplates.students(), username, password, newStudent);
                document.getElementById('student-form').reset();
                document.getElementById('validation').innerHTML = '';
            }
        }
        catch (er) {
            window.alert(er.message);
        }
    }
};