let timeout;

document.getElementById('search').addEventListener('input', function () {
	clearTimeout(timeout);

	const query = this.value;
	const searchSource = this.dataset.source || "1";
	const pagination = document.getElementById('filmPagination');

	if (query.length > 0) {
		timeout = setTimeout(() => {
			fetch(`/Home/SearchFilm?query=${query}&searchSource=${searchSource}`)
				.then(response => response.json())
				.then(data => {
					const resultDiv = document.getElementsByClassName('film-list')[0];
					resultDiv.innerHTML = "";
					if (searchSource === "1")
						pagination.style.display = "none";

					console.log(data);

					if (data && data.length > 0) {
						data.forEach(film => {
							const div = document.createElement('div');
							div.className = 'film-card';

							if (searchSource === "0") {
								// Arşive ekleme versiyonu
								div.innerHTML = `
								<form method="post" action="/Home/AddToArchive">
									<img src="${film.poster}" alt="${film.title}">
									<div class="film-info">
										<h3>${film.title} (${film.year})</h3>
										<input type="hidden" name="imdbId" value="${film.imdbID}" />
										<input type="hidden" name="searchSource" value="0" />
										<button type="submit" class="btn">Arşive Ekle</button>
									</div>
								</form>
								`;
							} else {

								div.innerHTML = `
								<a href="/Home/FilmDetail/${film.id}">
									<img src="${film.poster}" alt="${film.title}">
								</a>
								<a href="/Home/FilmDetail/${film.id}">
									<div class="film-info">
										<h3>${film.title} (${film.year})</h3>
										<p>${film.plot.slice(0, 50)} ...</p>
									</div>
								</a>
								`;
							}

							resultDiv.appendChild(div);
						});
					} else {
						resultDiv.innerHTML = "<div>Film Bulunamadı</div>";
					}
				})
				.catch(error => {
					console.log("Hata: " + error);
				});
		}, 500);
	} else if (searchSource == "1") {
		window.location.href = "/Home/Index";
	}
});