<template>
  <l-map
      :zoom="zoom"
      :center="center"
       class="news-map"
      >
      <l-tile-layer
        :url="url"
        :attribution="attribution"/>
      <!-- <v-marker-cluster :options="clusterOptions"> -->
         <l-marker
         v-for="(feedItem, index) in feedItems"
         :key="index"
         :lat-lng="feedItem.coordinates">
        <l-icon
          :icon-size="[48, 48]"
          :icon-anchor="[16, 37]">
          <img :src="iconMap[feedItem.category]" style="height: 32px; width: 32px;" />
        </l-icon>
         <l-popup>
           <a class="title is-6" :href="feedItem.link" target="_blank">
              {{feedItem.title}}
              <div class="subtitle is-6">
             {{feedItem.channelTitle}}
           </div>
            </a>
        </l-popup>
       </l-marker>
      <!-- </v-marker-cluster> -->
    </l-map>
</template>

<script>

import {
  L,
  LMap,
  LTileLayer,
  LMarker,
  LIcon,
  LPopup,
} from 'vue2-leaflet';

import markerIcons from '../assets/markerIcons';

export default {
  name: 'news-map',
  mounted() {
  },
  data() {
    return {
      zoom: 2,
      center: L.latLng(47.413220, -1.219482),
      url: 'https://{s}.tile.openstreetmap.fr/hot/{z}/{x}/{y}.png',
      attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>, Tiles courtesy of <a href="http://hot.openstreetmap.org/" target="_blank">Humanitarian OpenStreetMap Team</a>',
      icon: L.icon({
        iconUrl: 'img/helmet.png',
        iconSize: [32, 32],
        iconAnchor: [16, 37],
      }),
      oms: {},
      clusterOptions: {
        maxZoom: 10,
        imagePath: 'img',
      },
      iconMap: markerIcons,
    };
  },
  props: ['feedItems'],
  components: {
    LMap, LTileLayer, LMarker, LIcon, LPopup, // 'v-marker-cluster': Vue2LeafletMarkerCluster,
  },
};
</script>

<style scoped>
@import "~leaflet.markercluster/dist/MarkerCluster.css";
@import "~leaflet.markercluster/dist/MarkerCluster.Default.css";

  .news-map {
    min-height: 170px;
}

</style>
