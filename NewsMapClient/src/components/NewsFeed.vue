<template>
<div class="feed-container">
  <header class="panel-header">
    <strong>News Feed</strong> ({{feedItems.length}})
    <img v-if="loading" class="is-pulled-right small-loader"
     src="../assets/loaders/Rolling-1s-24px.svg" />
  </header>
  <div class="feed-body">
    <news-card
    v-for="(feedItem,index) in feedItems"
    :key="index"
    :feedItem="feedItem"/>
    <div class="big-loader">
       <img ref="bigLoader" v-if="feedItems.length == 0"
        src="../assets/loaders/Ripple-1s-143px.svg" />
    </div>
  </div>
</div>
</template>

<script>

import NewsCard from './NewsCard.vue';

export default {
  name: 'news-feed',
  data() {
    return {
      newsBody: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus nec iaculis mauris.',
    };
  },
  props: ['feedItems', 'loading'],
  components: { NewsCard },
};
</script>

<style scoped>
.feed-container {
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    flex-wrap: nowrap;
}
header {
    flex-shrink: 0;
    font-size: 1.2em;
    padding-bottom: 0.5em;
    margin-bottom: 1px;
    border-bottom: 1px solid black
}
.feed-body{
    flex-grow: 1;
    overflow: auto;
    min-height: 2em;
}
.small-loader {
  margin-top: 2%;
}
.big-loader {
 margin-top: 50%;
 text-align: center;
}
</style>
