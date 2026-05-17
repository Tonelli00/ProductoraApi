export function renderSkeletons(container, count) {
  container.innerHTML = Array.from({ length: count }).map(() => `
    <div class="bg-white border border-gray-200 rounded-xl overflow-hidden animate-pulse">
      <div class="relative h-32 w-full bg-gray-200">
        <div class="absolute bottom-2.5 left-3 h-5 w-16 rounded-full bg-gray-300"></div>
      </div>
      <div class="p-3 flex flex-col gap-2">
        <div class="h-3 w-2/5 rounded bg-gray-200"></div>
        <div class="h-4 w-4/5 rounded bg-gray-200"></div>
        <div class="flex items-center gap-1.5">
          <div class="h-2.5 w-2.5 rounded-full bg-gray-200"></div>
          <div class="h-2.5 w-3/5 rounded bg-gray-200"></div>
        </div>
        <div class="border-t border-gray-100 my-0.5"></div>
        <div class="flex justify-between items-center">
          <div class="h-3.5 w-1/4 rounded bg-gray-200"></div>
          <div class="h-8 w-20 rounded-lg bg-gray-200"></div>
        </div>
      </div>
    </div>
  `).join("");
}