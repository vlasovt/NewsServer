/* eslint-disable global-require */
import Vue from 'vue';
import Buefy from 'buefy';
import { L } from 'vue2-leaflet';
import App from './App.vue';
import router from './router';
import store from './store';
import './registerServiceWorker';
import 'leaflet.icon.glyph';
import dateFormatFilter from './filters/FormatDate';

import 'leaflet/dist/leaflet.css';
import 'buefy/dist/buefy.css';

// eslint-disable-next-line no-underscore-dangle
delete L.Icon.Default.prototype._getIconUrl;

L.Icon.Default.mergeOptions({
  iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
  iconUrl: require('leaflet/dist/images/marker-icon.png'),
  shadowUrl: require('leaflet/dist/images/marker-shadow.png'),
});

Vue.config.productionTip = false;

Vue.use(Buefy);
Vue.filter('formatDate', dateFormatFilter);

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app');
