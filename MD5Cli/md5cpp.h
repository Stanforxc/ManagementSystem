#pragma once
#include <cstdint>
#include <memory>
#include <string>

#ifdef MD5CPP_EXPORTS
#define MD5CPP_API __declspec(dllexport)
#else
#ifndef MD5CPP_STATIC
#define MD5CPP_API __declspec(dllimport)
#else
#define MD5CPP_API /**/
#endif
#endif

namespace md5cpp {

	class MD5CPP_API md5 {
	private:
		uint32_t m_buf[4];
		uint32_t m_bits[2];
		uint32_t m_in[16];
		bool     m_finalized;

	public:
		//! initialize
		md5();

		// * = default; All copy/move operations OK!

		// TODO: init + finalize ctor from string / vector

		// TODO: update with string / vector

		// TODO: finalize with return vector of uint8_t

		// TODO: m_is_finalized flag

		// TODO: as_hex with hex-string return

		// TODO: W4 !

		//! Start MD5 accumulation.  Set bit count to 0 and buffer to mysterious
		//! initialization constants.
		void initialize();

		//! Update context to reflect the concatenation of another buffer full
		//! of bytes.
		void update(const uint8_t* buf, uint32_t len);
		void update(const std::string& data);

		template<typename Iterator>
		void update(Iterator begin, Iterator end) {
			typedef std::iterator_traits<Iterator>::iterator_category it_cat;
			static_assert(std::is_base_of<std::random_access_iterator_tag, it_cat>::value, "Iterator category must be random access!");
			const int len = static_cast<int>(end - begin);
			if (len > 0) {
				update(&*begin, len);
			}
		}

		//! Finalize hash into `out_digest`, if provided (must have room for 16 bytes!)
		void finalize(uint8_t* out_digest = nullptr);

		//! return finalized hash into `out_digest` (must have room for 16 bytes!)
		void get_hash(uint8_t* out_digest);

	private:
		void transform();
		void check_finalized(bool expect_finalized, const char* context);
	};

}

