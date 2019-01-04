<template>
  <div id="app">
    <header>
      <div class="column">
        <nav
          class="navbar is-fixed-top"
          style="z-index: 2000"
          role="navigation"
          aria-label="main navigation"
        >
          <div class="navbar-brand">
            <router-link class="navbar-item" to="/">
              <img src="./assets/logo.png">
            </router-link>
            <a
              role="button"
              class="navbar-burger burger"
              :class="{'is-active': isActive}"
              @click="isActive = !isActive"
              aria-label="menu"
              aria-expanded="false"
              data-target="topNavbar"
            >
              <span aria-hidden="true"></span>
              <span aria-hidden="true"></span>
              <span aria-hidden="true"></span>
            </a>
          </div>
          <div id="topNavbar" class="navbar-menu" :class="{'is-active': isActive}">
            <div class="navbar-start"></div>
            <div class="navbar-end">
              <router-link class="navbar-item" to="/about">About</router-link>
              <div class="navbar-item">
                <div class="buttons">
                  <button class="button is-primary">
                    <strong>Sign up</strong>
                  </button>
                  <button class="button is-light">Log in</button>
                </div>
              </div>
            </div>
          </div>
        </nav>
      </div>
    </header>
    <main>
      <div class="section">
        <router-view/>
      </div>
    </main>
  </div>
</template>

<script>
export default {
  name: 'app',
  data() {
    return {
      isActive: false,
    };
  },
  components: {},
};
</script>

<style scoped>
  .html {
    height: 100%;
  }
  .section {
    height: 95vh;
    padding-bottom: 10px;
  }
</style>
