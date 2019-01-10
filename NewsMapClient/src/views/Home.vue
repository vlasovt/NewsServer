<template>
    <div class="columns is-multiline" style="height: 100%;">
      <div class="column is-four-fifths">
        <news-map :feedItems="feedItems" />
      </div>
      <div class="column is-one-fifth">
        <news-feed :feedItems="feedItems" :loading="loading" />
      </div>
    </div>
</template>

<script>
// @ is an alias to /src
import { mapActions } from 'vuex';
import NewsMap from '../components/NewsMap.vue';
import NewsFeed from '../components/NewsFeed.vue';

export default {
  name: 'home',
  created() {
    this.getFeed();
    this.pollData();
  },
  beforeDestroy() {
    clearInterval(this.polling);
  },
  data() {
    return {
      isActive: false,
    };
  },
  methods: {
    ...mapActions(['getFeed']),
    pollData() {
      this.polling = setInterval(() => {
        this.getFeed();
      }, 3600000);
    },
  },
  computed: {
    feedItems() {
      if (this.$store.state.feed) {
        return this.$store.state.feed;
      }
      return [];
    },
    loading() {
      return this.$store.state.loadingFeeds && this.$store.state.feed;
    },
  },
  components: {
    NewsMap, NewsFeed,
  },
};
</script>
