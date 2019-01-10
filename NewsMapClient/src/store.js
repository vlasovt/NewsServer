import Vue from 'vue';
import Vuex from 'vuex';
import axios from 'axios';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    feed: null,
    loadingFeeds: false,
  },
  mutations: {
    updateFeed(state, feed) {
      if (feed && feed.length > 0) {
        state.feed = feed;
      }
      state.loadingFeeds = false;
    },
  },
  actions: {
    getFeed({ commit, state }) {
      // eslint-disable-next-line no-param-reassign
      state.loadingFeeds = true;
      axios.get('/api/news')
        .then(result => commit('updateFeed', result.data))
        .catch(console.error);
    },
  },
});
