import { createApp } from "vue";
import App from "./App.vue";

import { BaseButton } from "@/components/base/button";
import BaseInput from "@/components/base/BaseInput";
import { BaseComboBox, BaseComboBoxOption } from "@/components/base/comboBox";
import BasePagination from "@/components/base/BasePagination.vue";
import BasePopup from "@/components/base/BasePopup.vue";
import BaseLoader from "@/components/base/BaseLoader.vue";
import BaseToastMessage from "@/components/base/BaseToastMessage.vue";
import { MISATable } from "@/components/base/tableV2";
import MISAPopup from "@/components/base/MISAPopup";
import BaseRadio from "@/components/base/BaseRadio";
import BaseSelectBox from "@/components/base/BaseSelectBox";
import { BaseChart, BasePieChart } from "@/components/base/chart";

import "@/utils/formatString";

import router from "./router";

import "devextreme/dist/css/dx.light.css";

const app = createApp(App);

app.use(router);

app.component(BaseButton.name, BaseButton);
app.component(BaseInput.name, BaseInput);
app.component(MISATable.name, MISATable);
app.component(BaseComboBox.name, BaseComboBox);
app.component(BaseComboBoxOption.name, BaseComboBoxOption);
app.component(BasePagination.name, BasePagination);
app.component(MISAPopup.name, MISAPopup);
app.component(BasePopup.name, BasePopup);
app.component(BaseLoader.name, BaseLoader);
app.component(BaseToastMessage.name, BaseToastMessage);
app.component(BaseRadio.name, BaseRadio);
app.component(BaseSelectBox.name, BaseSelectBox);
app.component(BaseChart.name, BaseChart);
app.component(BasePieChart.name, BasePieChart);

app.mount("#app");
