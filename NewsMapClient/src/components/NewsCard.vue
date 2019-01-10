<template>
<a :href="feedItem.link">
  <div class="card">
    <header class="card-header">
      <p class="card-header-title">
      {{feedItem.title}}
      </p>
    </header>
    <div class="card-content">
      <strong>{{feedItem.date | formatDate}}:
         </strong>{{feedItem.description}}
          <strong>{{feedItem.channelTitle}}</strong>
    </div>
  </div>
</a>
</template>

<script>
export default {
  name: 'news-card',
  props: ['feedItem'],
  components: {
  },
};
</script>

<style scoped>
.card-header{
  background-color: lightgray;
}
.card-header-title{
  padding: 5px;
}
.card-content{
  padding: 5px;
}
</style>
