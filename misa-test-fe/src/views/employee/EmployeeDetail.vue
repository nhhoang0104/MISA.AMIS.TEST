<template lang="html">
  <div class="modal"></div>
  <div class="dialog">
    <div class="dialog__header">
      <div class="dialog__header__title">
        <div class="title">Thông tin nhân viên</div>
      </div>
      <div class="dialog__header__close">
        <div class="icon icon--24 icon-help m-r-6"></div>
        <div class="icon icon--24 icon-close"></div>
      </div>
    </div>
    <div class="dialog__content">
      <div class="body">
        <div class="flex p-b--18">
          <div class="w-50 p-r--26">
            <div class="flex row-input">
              <div class="w-40 p-r--6">
                <base-input
                  :hasLabel="true"
                  label="Mã"
                  :required="true"
                ></base-input>
              </div>
              <div class="w-60">
                <base-input
                  :hasLabel="true"
                  label="Tên"
                  :required="true"
                ></base-input>
              </div>
            </div>
            <div class="row-input">
              <div class="w-100">
                <base-input
                  :hasLabel="true"
                  label="Đơn vị"
                  :required="true"
                ></base-input>
              </div>
            </div>
            <div class="row-input">
              <div class="w-100">
                <base-input :hasLabel="true" label="Chức danh"></base-input>
              </div>
            </div>
          </div>
          <div class="w-50">
            <div class="flex row-input">
              <div class="w-40 p-r--6">
                <base-input :hasLabel="true" label="Ngày sinh"></base-input>
              </div>
              <div class="w-60">
                <base-input :hasLabel="true" label="Giới tính"></base-input>
              </div>
            </div>
            <div class="flex row-input">
              <div class="w-60 p-r--6">
                <base-input :hasLabel="true" label="Số CMND"></base-input>
              </div>
              <div class="w-40">
                <base-input :hasLabel="true" label="Ngày cấp"></base-input>
              </div>
            </div>
            <div class="row-input">
              <div class="w-100">
                <base-input :hasLabel="true" label="Nơi cấp"></base-input>
              </div>
            </div>
          </div>
        </div>
        <div>
          <div class="row-input">
            <div class="w-100">
              <base-input :hasLabel="true" label="Địa chỉ"></base-input>
            </div>
          </div>

          <div class="flex row-input">
            <div class="w-25 p-r--6">
              <base-input :hasLabel="true" label="ĐT di động"></base-input>
            </div>
            <div class="w-25 p-r--6">
              <base-input :hasLabel="true" label="ĐT cố định"></base-input>
            </div>
            <div class="w-25">
              <base-input :hasLabel="true" label="Email"></base-input>
            </div>
          </div>

          <div class="flex row-input">
            <div class="w-25 p-r--6">
              <base-input
                :hasLabel="true"
                label="Tài khoản ngân hàng"
              ></base-input>
            </div>
            <div class="w-25 p-r--6">
              <base-input :hasLabel="true" label="Tên ngân hàng"></base-input>
            </div>
            <div class="w-25">
              <base-input :hasLabel="true" label="Chi nhánh"></base-input>
            </div>
          </div>
        </div>
      </div>
      <div class="footer">
        <div class="divine"></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style lang="css"></style>
