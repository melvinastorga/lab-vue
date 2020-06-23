<template>

  <div style="margin: 0 auto; width: 80%">
    <Panel header="LAB VUE: STUDENT LIST">
      <DataTable :value="students" :filters="filters" :paginator="true" :rows="2">
        <Column field="studentId" header="Id"></Column>
        <Column field="name" header="Name"></Column>
        <Column field="age" header="Age"></Column>
        <Column field="majorName" header="Major"></Column>
        <Column field="nationalityName" header="Nationality"></Column>
      </DataTable>
    </Panel>
  </div>
</template>>

<script>
import StudentService from "../Services/StudentService";
export default {
  name: "CrudApp",
  data() {
    return {
      students: null
    };
  },
  studentService: null,
  created() {
    this.studentService = new StudentService();
  },
  mounted() {
    this.studentService.getAll().then(data => {
      this.students = data.data;
    });
  }
};
</script>>

<style>
</style>>