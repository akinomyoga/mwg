#include "stdafx.h"
#include "history1.h"
#ifdef _MANAGED
#	pragma unmanaged
#endif
namespace mwg{
namespace Compression{
	void initial_cache(byte* cache){
		std::srand(10);
		for(int i=0;i<SZ_CACHE;i++){
			cache[i]=std::rand();
		}
	}
	int hash(uint value){
		int w=32;
		uint v=0;
		while(w>0){
			v^=value;
			value>>=B_TABLE;
			w-=B_TABLE;
		}
		return MASK_TABLE&v;
	}
	int hash(int value){
		return hash((uint)value);
	}
	int hash(byte (&val)[REF_LEN_BASE]){
		assert(REF_LEN_BASE==4);
		uint a=*(uint*)val;
		return hash(a);
	}
}
}
