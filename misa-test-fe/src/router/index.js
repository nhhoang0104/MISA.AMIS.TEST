import { createRouter, createWebHistory } from "vue-router";
import EmployeeList from "@/views/employee/EmployeeList";

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
  ],

  linkActiveClass: "menu-item--active",
});

export default router;
