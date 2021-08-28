import { createApp } from "vue";
import App from "./App.vue";

const app = createApp(App);

import { BaseButton } from "@/components/base/button";
import BaseInput from "@/components/base/BaseInput";
import {
  BaseTable,
  BaseTableHead,
  BaseTableBody,
} from "@/components/base/table";

app.component(BaseButton.name, BaseButton);
app.component(BaseInput.name, BaseInput);

app.component(BaseTable.name, BaseTable);
app.component(BaseTableHead.name, BaseTableHead);
app.component(BaseTableBody.name, BaseTableBody);

app.mount("#app");
