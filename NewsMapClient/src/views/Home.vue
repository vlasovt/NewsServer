<template>
    <div class="columns is-multiline" style="height: 100%;">
      <div class="column is-four-fifths">
        <news-map :feedItems="feedItems" />
      </div>
      <div class="column is-one-fifth">
        <news-feed :feedItems="feedItems" />
      </div>
    </div>
</template>

<script>
// @ is an alias to /src
import { mapActions } from 'vuex';
import NewsMap from '@/components/NewsMap.vue';
import NewsFeed from '@/components/NewsFeed.vue';

export default {
  name: 'home',
  created() {
    this.getFeed();
  },
  data() {
    return {
      isActive: false,
    };
  },
  methods: {
    ...mapActions(['getFeed']),
  },
  computed: {
    feedItems() {
      if (this.$store.state.feed) {
        return this.$store.state.feed;
      }
      return [];
    },
  },
  components: {
    NewsMap, NewsFeed,
  },
};
</script>
