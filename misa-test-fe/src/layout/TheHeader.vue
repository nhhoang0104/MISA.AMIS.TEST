<template lang="html">
  <div class="header">
    <div class="header__menu">
      <div class="header__menu__option"></div>
      <div class="header__menu__logo">
        <img
          src="@/assets/img/Logo_Module_TiengViet_White.66947422.svg"
          class="img-icon"
        />
      </div>
    </div>
    <div class="header__content">
      <div class="header__content__left">
        <div class="icon icon-bars icon--24 header-icon"></div>
        <div class="branch">
          <div class="branch__content">CÔNG TY CỔ PHẦN MISA</div>
          <div class="branch__toggle">
            <div
              class="icon icon-chevron-right icon--14 icon--r-90 header-icon"
            ></div>
          </div>
        </div>
      </div>
      <div class="header__content__right">
        <div class="notify">
          <div class="icon icon--24 header-icon icon-bell"></div>
        </div>
        <div class="account-info">
          <div class="user-ava">
            <div class="icon icon--32 icon-user-ava icon-circle"></div>
          </div>
          <div class="user-name">Nguyễn Huy Hoàng</div>
          <div class="icon icon-chevron-down icon--14"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "the-header",
};
</script>

<style lang="css">
@import url("../assets/css/layout/Header.css");
</style>
