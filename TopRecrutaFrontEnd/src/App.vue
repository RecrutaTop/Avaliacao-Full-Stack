<script setup>
import { onMounted, ref } from 'vue'

const fornecedores = ref([])

async function buscarFornecedores() {
  const response = await fetch('https://localhost:7277/api/fornecedores')
  fornecedores.value = await response.json()
}

onMounted(() => {
  buscarFornecedores()
})
</script>

<template>
  <h1 class="text-3xl font-bold">Fornecedores</h1>
  <pre>
    {{ fornecedores }}
  </pre>
</template>

<style scoped></style>
