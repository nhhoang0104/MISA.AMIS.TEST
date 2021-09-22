import { createRouter, createWebHistory } from "vue-router";
import EmployeeList from "@/views/employee/EmployeeList";
import EmployeeReport from "@/views/employee/EmployeeReport";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/",
      name: "Home",
      redirect: "/dashboard",
    },
    {
      path: "/dashboard",
      name: "EmployeeList",
      component: EmployeeList,
    },
    {
      path: "/cash",
      name: "EmployeeReport",
      component: EmployeeReport,
    },
  ],

  linkActiveClass: "menu-item--active",
});

export default router;
