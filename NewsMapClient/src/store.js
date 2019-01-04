import Vue from 'vue';
import Vuex from 'vuex';
import axios from 'axios';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    feed: null,
  },
  mutations: {
    updateFeed(state, feed) {
      // eslint-disable-next-line no-param-reassign
      state.feed = feed;
    },
  },
  actions: {
    getFeed({ commit }) {
      axios.get('/api/news')
        .then(result => commit('updateFeed', result.data))
        .catch(console.error);
    },
  },
});
