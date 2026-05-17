export function renderSkeletons(container, count) {
  container.innerHTML = Array.from({ length: count }).map(() => `
    <div class="bg-white border border-gray-200 rounded-xl overflow-hidden animate-pulse">
      <div class="bg-gray-200 h-32 w-full"></div>
      <div class="p-3 space-y-2">
        <div class="bg-gray-200 h-3 w-2/5 rounded"></div>
        <div class="bg-gray-200 h-4 w-4/5 rounded"></div>
        <div class="bg-gray-200 h-3 w-3/5 rounded"></div>
        <div class="bg-gray-200 h-8 w-full rounded-lg mt-1"></div>
      </div>
    </div>
  `).join("");
}