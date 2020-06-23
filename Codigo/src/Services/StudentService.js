import axios from 'axios';

export default class StudentService {

    url = "https://localhost:44376/api/student/";

    getAll() {
        return axios.get(this.url + "GetAllStudentsSP");
}

}