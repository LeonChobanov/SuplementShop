<template>
  <v-container class="home">
    <div class="category-grid" v-if="!loading && !error">
      <CategoryCard
        v-for="c in categoryState.categories"
        :key="c.id"
        :title="c.name"
        :image="c.imageUrl"
      ></CategoryCard>
    </div>
    <div v-if="categoryState.loading">
      <div class="text-center">
        <v-progress-circular indeterminate  variant="primary" label="Text Centered" ></v-progress-circular>
      </div>
    </div>
  </v-container>
</template>

<script>
import CategoryCard from "@/components/CategoryCard.vue";

export default {
  name: "Home",
  components: {
    CategoryCard
  },
  mounted() {
    this.$store.dispatch("categoryState/loadCategories");
  },
  computed: {
    categoryState() {
      return this.$store.state.categoryState;
    },
    loading() {
      return this.categoryState.loading;
    },
    categories() {
      return this.categoryState.categories;
    },
    error() {
      return this.categoryState.error;
    }
  }
};
</script>

<style lang="scss" scoped>
.category-grid {
  display: grid;
  align-items: center;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  grid-gap: 2rem;
  margin: auto;
  padding: 2rem 2rem;
}
</style>